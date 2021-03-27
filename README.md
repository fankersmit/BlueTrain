# BlueTrain

Project demonstrating the possibilities of ansering a question, by visiting different terminals with information holding and processing power.
It is inspired by he vision of the Dutch PHT project.
It concentrates on the routing and ordering problems in a distributed environment.
Possible uses are data processing without actually having the data yourself. 

# The story

A ***question*** is packaged into a ***container***. The container travels through any number of ***terminals***. 
This is called a ***journey***. Each terminal adds information or processes information which together constitutes the answer. 
Traveling from one terminal to another is called a ***trip***. The journey itself is logged onto a ***routing slip***.

# Terminal

A terminal is accessible through its REST-API. This project provides the interface definition and one implementation.

## Sending a container

A ***Terminal*** can ***Send*** and ***Receive*** any ***Container***. 
A Terminal can only receive a contiainer when **Open**.
A ***failing*** Send means the container stays in the ***HoldingYard**.
You can Send to a specfic terminal.
You can send to the next terminal as determinend from the ***Routingslip***.

# Container

A container holds the ***Cargo*** to be processed inthe containers on its ***Routingslip***.
Cargo is always readable in a human form like a docker docker defnition file.
A container wihthout a routing slip doesn't know where to go.
A container contaiener has no knowledge of the terminal it is in.

# RoutingSip
A ***Routingslip*** describes all the terminals a contaitner has to visit.
A ***Trip*** is the moving from a container from one terminla to the next. 
A ***Journey*** is an **ordered list** of trips

# Trip
A Trip contains a Departure, Destination and an indication if the trip is realized.




# BlueTrain classdiagram
```mermaid

classDiagram
    
    class Terminal {
        +Information Information
        +Status enum
        +Capabilities Capabilities 
        +Open()  void
        +Close() void
        +Receive( Container c ) bool 
        +Send( Terminal t,  Container c) bool  
        +Remove( Container c) bool
        +IsOpen() bool 
        +IsProcessing() bool
        +IsReceiving() bool 
        +IsSending() bool
    }
    
    class Trip {
        +Departure ITerminalInfomation
        +Destination ITerminalInformation
        +TimeOfDeparture  DateTime
        +TimeOfArrival DateTime
        +IsRealized bool
    }

    class Capabilities {

    }
    
    class ITerminal {
        << Interface >>
    }
    
    class HoldingYard {
        +Capacity int
        +IsEmpty() bool
        +IsFilled() bool    
    }
    
    class Container {
        +RoutingSlip RoutingSlip
        +Cargo Cargo
    }

    class RoutingSlip {
        +Created DateTime
        +Journey ListOfTrips
    }

    class ContainterHandler {
        +Handle( Container c ) Containter 
    }
    
    class Information {
        +Name string
        +Description string 
        +ID Guid 
    }
    
    class TerminalStatus {
        << enumeration >>
        Closed
        Opening
        Open
        Closing
    }
    
    Container o-- RoutingSlip
    Container o-- Cargo
    TerminalInformation <|-- Information
    Terminal <|.. ITerminal
    Terminal *.. HoldingYard
    Terminal *..TerminalInformation
    Terminal *..ContainerHandler 

