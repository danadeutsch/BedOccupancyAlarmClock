#include <msp430.h> 
#include <stdbool.h>       // Gives the true and false symbols
#include <iso646.h>        // Gives the and and or symbols
#include <stdint.h>        // Gives the uint32_t symbol


//FUNCTIONS
void ReadForce();
void setupHX711();
void ReadAverageForce(void);
void sendForceToSerial(void);
//VARIABLES
volatile unsigned long long int LoadCellVal;

//GLOBALS

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	setupHX711();
	while(1){
	    ReadAverageForce();
	    sendForceToSerial();
	    LoadCellVal = 0;
	}

	return 0;
}

void setupHX711(){
    P1DIR &= ~BIT0; // the in values
    P1DIR |= BIT1; // the clock signal
    P1OUT &= ~BIT1;
    P4OUT |= BIT0; //pull up
    P1REN |= BIT0; //enable pull up


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

}


void ReadAverageForce(){
    unsigned char count = 0;
    unsigned char numtoAve = 5;
    LoadCellVal = 0;
    for(count = numtoAve; count>0;count--){
        ReadForce();
    }

    LoadCellVal = LoadCellVal/numtoAve;
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

    LoadCellVal += ReadVal;
}

void sendForceToSerial(){
    unsigned long sending = LoadCellVal;
    char data;
    data = 125;
    UCA0TXBUF = data;
    while ((UCA0IFG & UCTXIFG)==0);
    data = sending >>16;
    UCA0TXBUF = data;
    while ((UCA0IFG & UCTXIFG)==0);
    data = sending >>8;
    UCA0TXBUF = data;
    while ((UCA0IFG & UCTXIFG)==0);
    data = sending;
    UCA0TXBUF = data;
    while ((UCA0IFG & UCTXIFG)==0);
}
