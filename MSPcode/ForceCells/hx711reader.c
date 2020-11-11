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
	    __delay_cycles(1000);
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

void ReadForce(){
    unsigned char counter =0;
    unsigned long ReadVal=0;

    while(P1IN & BIT0){};
    ReadVal =0;

    for(counter = 0 ; counter <24;counter++){
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
