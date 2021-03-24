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
    
    class Capabilities
    
    class ITerminal {
        << Interface >>
    }
    
    class HoldingYard {
        +Capacity int
        +IsEmpty() bool
        +IsFilled() bool    
    }
    
    class Container
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
    
    Terminal <|.. ITerminal
    Terminal *.. HoldingYard
    Terminal *..Information
