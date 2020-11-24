#include <msp430.h> 
#include <stdbool.h>       // Gives the true and false symbols
#include <iso646.h>        // Gives the and and or symbols
#include <stdint.h>        // Gives the uint32_t symbol

//FUNCTIONS
void generalAlarmsSetup(void);
void checkForSoundState(void);
void bedCheck1(void);
void outOfBedState(void);
void bedCheck2(void);
void ReadForce();
void setupHX711();
void ReadAverageForce(void);


//VARIABLES
volatile bool bedOccupied = false;
volatile bool soundSensed = false;
volatile unsigned int timercounter = 0; // count var for timer
volatile unsigned int waitingTime = 0;  // number of counts for the full timer cycle
volatile char state = 0;
volatile char counterForOutCheck = 0;

#define BUZZERPORT P3OUT
#define BUZZERPIN BIT5
#define BEDPIN  BIT4
#define NOIZEPIN BIT5
#define NOISEPORT P2IN
#define BEDPORT P1IN

#define SNOOZETIME 18  //in seconds
#define CHECKTIME 30   //in seconds
#define OUTOFBEDPASS 12 //in check time intervals
#define TONE1 120
#define TONE2 50

volatile int tone = TONE1;
#define BEDLIMIT 8800000
volatile unsigned long long int LoadCellVal;

/**
 * main.c
 */
int main(void)
{
    WDTCTL = WDTPW | WDTHOLD;   // stop watchdog timer
    generalAlarmsSetup();
    setupHX711();
     __enable_interrupt(); // enable global interrupt

    while(1){
        switch(state){
        case 0:
            checkForSoundState();
            break;
        case 1:
            bedCheck1();
            break;
        case 2:
            break;
        case 3:
            outOfBedState();
            break;
        case 4:
            bedCheck2();
            break;
        case 5:
            break;
        case 6:
            break;
        case 7:
            break;
        }
    };
    return 0;
}


//STATE 0
void checkForSoundState(){
    P1OUT &= ~BIT3; // ending the cycle light
    TB1CCTL1 &= ~CCIE;      //disable TB ISR
    P4IE &= ~BIT0;          //disable snooze interrupt
    P1IE |= BIT4;          //enable noise sensor interrupt
    state = 7;
    //noise ISR will change so something else that would also be an isr probable
    //Clear all variables for next round
}

//STATE 7
// ISR for polling the noise of the phone alarm
#pragma vector=PORT1_VECTOR
__interrupt void port_1(void){
        P1OUT &= ~BIT3; // starting the cycle light
        state = 1;
        P1IFG &= ~BIT4;
        P1IE &= ~BIT4; // clear this ISR flag

}
//STATE 1
void bedCheck1(void){
    //Perform the collection of ADC for force sensor
    ReadForce();
    if(LoadCellVal > BEDLIMIT){ // bed high aka occupied
        state =2;
        TB1CTL |= TBCLR;            // clear TB clock
        TB1CCTL1 |= CCIE;           // enable TB counter
        waitingTime = SNOOZETIME;   //set the timer to count up to the snooze time
        timercounter =0;
    }else{
        counterForOutCheck =0;
        state = 3;
    }
}


//STATE 5
//ISR for Snooze Button to turn off alarm and start the snooze timer ISR after clearing its clock
#pragma vector=PORT4_VECTOR
__interrupt void port_4(void){

    BUZZERPORT &= ~BUZZERPIN;   // alarm off
    P1DIR &= ~BIT5;
    P4IFG &= ~BIT0;             // clear this ISR flag
    P4IE |= BIT0;               //disable snooze interrupt
    state = 2;
    timercounter =0;
    TB1CTL |= TBCLR;            // clear TB clock
    TB1CCTL1 |= CCIE;           // enable TB counter
    waitingTime = SNOOZETIME;
}

//STATE 2 and 6
//Snooze timer checks if person in bed and activated alarm if in bed,
//  and if out of bed disable this ISR
#pragma vector = TIMER1_B1_VECTOR
__interrupt void TimerB1_snooze(void){

    if(state==5){
        switch(tone){
        case TONE1:
            tone = TONE2;
            break;
        case TONE2:
            tone = TONE1;
            break;
        }

        TB0CCR0 =tone;
        TB0CCR2 = tone/2;
    }
    if(timercounter == waitingTime){// when counting time is up
        if(state == 6){ // out of bed check
            //TB1CCTL1 &= ~CCIE;          // disable TB counter
            counterForOutCheck++;
            state = 4;
        }else if(state == 2){ //in bed check
            ReadForce();
            if(LoadCellVal > BEDLIMIT){ // bed high aka occupied
                BUZZERPORT |= BUZZERPIN;    // buzzer light on
                P1DIR |= BIT5;              // buzzer on
                state = 5;
                P4IE |= BIT0;               //enable snooze interrupt
            }else {
                counterForOutCheck =0;
                state = 3;
            }
        }
    }


    timercounter++;
    TB1CCTL1 &= ~CCIFG; //Clear TB flag
}

//STATE 3
void outOfBedState(void){
    waitingTime = CHECKTIME;  //set the timer to check
    TB1CTL |= TBCLR;            // clear TB clock
    TB1CCTL1 |= CCIE;           // enable TB counter
    timercounter = 0;
    state = 6;
}

//STATE 4
void bedCheck2(void){
    if(counterForOutCheck == OUTOFBEDPASS){
        state = 0;

    }else{
        ReadForce();
        if(LoadCellVal > BEDLIMIT){ // bed occupied
            P4IE |= BIT0;               //enable snooze interrupt
            BUZZERPORT |= BUZZERPIN;    // buzzer on
            P1DIR |= BIT5;
            counterForOutCheck = 0;
            state =5;
        }else{
            TB1CTL |= TBCLR;            // clear TB clock
            TB1CCTL1 |= CCIE;           // enable TB counter
            timercounter = 0;
            state = 6;
        }
    }
}


void ReadForce(){
    unsigned char counter =0;
    unsigned long ReadVal=0;

    while(P1IN & BIT0){};
    ReadVal =0;

    for(counter = 24 ; counter >0;counter--){
        P1OUT |= BIT1;
        ReadVal = ReadVal<<1;
        P1OUT &= ~BIT1;
        if(P1IN & BIT0){
             ReadVal |= 0x01;
        }
    }

    P1OUT |= BIT1;
    ReadVal = ReadVal ^ 0x800000;
    P1OUT &= ~BIT1;

    LoadCellVal = ReadVal;
}

void setupHX711(){
    P1DIR &= ~BIT0; // the in values
    P1DIR |= BIT1; // the clock signal
    P1OUT &= ~BIT1;
    P4OUT |= BIT0; //pull up
    P1REN |= BIT0; //enable pull up

}


void generalAlarmsSetup(void){
    //CONFIGURE CLOCKS
    //*********************
    CSCTL0_H = CSKEY_H;       // Clock password resister to allow writing

    // Set DCO to 8MHz
    CSCTL1 = 0;               // Clear DCO settings
    CSCTL1 &= ~DCORSEL;       // DCOR Mode Select 0
    CSCTL1 |= DCOFSEL_3;      // DCO selection frequency

    CSCTL2 = SELM_3 + SELA_3 + SELS_3;      // All clocks get source of DCO,!(32768hz)
    CSCTL3 = DIVS__32 + DIVA__1 + DIVM__32;    // set dividers to 1 or 32
    CSCTL0_H &= ~CSKEY_H;                   //lock the clock


    //CONFIGURE UART
    //**************
    //configure P2.0 ad P2.1 as the Rx and Tx for UART
    P2SEL1 |= BIT0 | BIT1;
    P2SEL0 &= ~(BIT0 | BIT1);

    UCA0CTLW0 = UCSWRST;    //Reset the eUSCI
    UCA0CTLW0 = UCSSEL0;    //set clock source ACLK

    // Configure the baud rate from the 8MHz of DCO to 9600
    UCA0BRW = 52;
    UCA0MCTLW |= 0x4900 + UCOS16 + UCBRF0;


    //CONFIGURE IO PINS FOR FAKE BED INPUTS
    P1DIR &= BEDPIN;
    P1OUT |= BEDPIN;
    P1REN |= BEDPIN;

    //CONFIGURE Noise sensor INPUT for ISR
    P1DIR &= ~BIT4; //input
    P1IES &= ~BIT4; // interrupt edge select low to high
    P1IE &= ~BIT4;  //enable noise sensor interrupt
    P1IFG &= ~BIT4; // clear interrupt

    //CONFIGURE BUZZER IO OUTPUT LED
    P3DIR |= BUZZERPIN;
    BUZZERPORT &= ~BUZZERPIN;

    //CONFIGURE SNOOZE BUTTON AS PB
    P4DIR &= ~BIT0; //input
    P4OUT |= BIT0; //pullup selected
    P4IES &= ~BIT0; // interrupt edge select low to high
    P4IE &= ~BIT0; // disenable port interrupt for now
    P4REN |= BIT0; //enable pullup/down resistors
    P4IFG &= ~BIT0; // clear interrupt

    //CONFIGURE TIMER B1 FOR 1s
    // wanted freq is 1hz
    TB1CTL |= TBSSEL_2 + ID__8;     //select the SMCLK and /8 divisor
    TB1CTL |= MC__UP;               // up mode to CTL0 and enable interrupt
    TB1EX0 |= TBIDEX__8;            // additional /8 divisor
    //TB1CTL &= ~TBIFG + TBIE; //no pending interrupts and disable it for now

    // f = 8000000/32/8/8 = 3906
    // f = 3906/3906aka 1sec
    TB1CCR0 =3906;
    TB1CCR1 = 1953;
    TB1CCTL1 |= OUTMOD_3;   // output on set reset
    TB1CCTL1 &= ~CCIE;       //dis-enable interrupt OG
    P3DIR |= BIT4;
    P3SEL0  |= BIT4;


    //CONFIGURE TIMER B0 FOR Buzzer
    // wanted freq is 1hz
    TB0CTL |= TBSSEL_2 + ID__8;     //select the SMCLK and /8 divisor
    TB0CTL |= MC__UP;               // up mode to CTL0 and enable interrupt

    // f = 8000000/32/8 = 31,250
    // f = 31250/310 = 100hz
    TB0CCR0 =tone;
    TB0CCR2 = tone/2;
    TB0CCTL2 |= OUTMOD_3;   // output on set reset
    TB1CCTL2 &= ~CCIE;       //dis-enable interrupt OG
    P1DIR &= ~BIT5;
    P1SEL0  |= BIT5;

    P1DIR |= BIT3;
    P1OUT &= ~BIT3;

}
