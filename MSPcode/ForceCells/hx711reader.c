#include <msp430.h> 
#include <stdbool.h>       // Gives the true and false symbols
#include <iso646.h>        // Gives the and and or symbols
#include <stdint.h>        // Gives the uint32_t symbol


//FUNCTIONS
void ReadForce();
void setupHX711();
void addBuffer1(char recieved);
long int removeBuffer1(void);
void ReadAverageForce(void);
void sendForceToSerial(void);
//VARIABLES
volatile unsigned long long int LoadCellVal;

#define length1 50
volatile long int buffer1[length1];
volatile unsigned char start1 = 0;
volatile unsigned char end1 = 0;
volatile bool writefull1 = false;
volatile bool readempty1 = true;

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
	    __delay_cycles(10000);
	}

	return 0;
}

void setupHX711(){
    P1DIR &= ~BIT0; // the in values
    P1DIR |= BIT1; // the clock signal
    P1OUT &= ~BIT1;
    P4OUT |= BIT0; //pull up
    P1REN |= BIT0; //enable pull up
}


void ReadAverageForce(){
    unsigned char count = 0;
    unsigned char numtoAve = 25;
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
    //ReadVal = ReadVal ^ 0x800000;
    P1OUT &= ~BIT1;

    LoadCellVal += ReadVal;
}


void addBuffer1(char recieved)
{
    if(writefull1 == false){

       if(start1 == end1)
           readempty1 = false;

        if (end1<length1){
            buffer1[end1] = recieved;
            end1++;
        }else { // at the end of the array
            end1=0;
            buffer1[end1] = recieved;
            end1++;
        }

        if(end1 == start1)
            writefull1 = true;
    }else{//when full send error message
        UCA0TXBUF = 255;
        while ((UCA0IFG & UCTXIFG)==0);
        UCA0TXBUF = 0;
        while ((UCA0IFG & UCTXIFG)==0);
    }

}


long int removeBuffer1(void){
    long int removed;

    if(readempty1 == false)
    {
        if(start1==end1)
            writefull1 = false;

        if (start1<length1){
            removed = buffer1[start1];
            start1++;
        }else { // at the end of the array
            start1=0;
            removed = buffer1[start1];
            start1++;
        }

        if(start1 == end1)
            readempty1= true;
    }else {// when empty
        UCA0TXBUF = 255;
        while ((UCA0IFG & UCTXIFG)==0);
        UCA0TXBUF = 0;
        while ((UCA0IFG & UCTXIFG)==0);
        removed = 255;
    }

    return removed;
}

void sendForceToSerial(){
    char data;
    data = 255;
    UCA0TXBUF = data;
    while ((UCA0IFG & UCTXIFG)==0);
    data = LoadCellVal >>16;
    UCA0TXBUF = data;
    while ((UCA0IFG & UCTXIFG)==0);
    data = LoadCellVal >>8;
    UCA0TXBUF = data;
    while ((UCA0IFG & UCTXIFG)==0);
    data = LoadCellVal;
    UCA0TXBUF = data;
    while ((UCA0IFG & UCTXIFG)==0);
}
