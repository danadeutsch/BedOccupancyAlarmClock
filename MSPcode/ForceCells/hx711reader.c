#include <msp430.h> 

//FUNCTIONS
void ReadForce();
void setupHX711();
//VARIABLES
volatile unsigned long int LoadCellVal;

//GLOBALS

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	setupHX711();
	while(1){
	    ReadForce();
	}

	return 0;
}

void setupHX711(){
    P1DIR &= ~BIT0; // the in values
    P1DIR |= BIT1; // the clock signal
    P1OUT &= ~BIT1;
    P1REN |= BIT0;
}

void ReadForce(){
    unsigned char counter =0;
    signed long ReadVal;

    while(P1IN & BIT0){};
    ReadVal =0;

    for(counter = 0 ; counter <24;counter++){
        P1OUT |= BIT1;
        ReadVal = ReadVal<<1;
        P1OUT &= ~BIT1;
        if(P1IN & BIT0){
            ReadVal++;
        }


    }

    P1OUT |= BIT1;
    ReadVal = ReadVal^ 0x800000;
    P1OUT &= ~BIT1;

    LoadCellVal = ReadVal;
}
