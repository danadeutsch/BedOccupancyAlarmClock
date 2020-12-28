# Bed Occupancy Alarm Clock

Many people struggle to get out of bed in the morning. Whether its because they fall back asleep after
turning oﬀ their alarms or because they lack motivation to get up it doesn’t matter. This product’s goal
is to get get the user out of bed using an alarm system that senses if the user is in bed. Since nowadays
people use their phones for everything, including for alarm clock, this product interfaces with the phone
using a sound sensor for detecting the phone alarm. This external phone alarm sound is detected in
order and begins a MSP430 controlled repetitive alarm system that would keep intermittently ringing
regardless of how many times you press the oﬀ button unless you leave the bed. Furthermore it will keep
monitoring the bed occupancy for an hour after you leave the bed to ensure you don’t return. The system
is design to get the user out of bed and remain out of bed.

The design uses two 50kg load cells positioned under a leg of the bed to sense the bed occupancy.
The voltage signal is ampliﬁed using the HX711 ampliﬁer and ADC that is collected by the MSP430
by bit banging. The MSP430 also interfaces with two other inputs that are used for creating the alarm
system: the snooze button and the sound sensor. It also uses a piezo buzzer to output the alarm sound
and its own internal timers and ISRs to control them all in a set state machine. The MSP430 controls
the system but it also sends data values via UART to a laptop for displaying key sleep cycle information in a C# .NET application.

