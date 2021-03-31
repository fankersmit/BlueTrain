

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

|done|style|CQ|path | verb | success | Error | body |description|
|:---:|:---:|:---:|:---|:---:|:---:|:---:|---:|:---|
|<span style="color:green">Yes</span>|RPC|Q|root/terminal/is-open|GET|200|400|json| Is terminal open and accepting  containers|
|<span style="color:green">Yes</span>|RPC|Q|root/terminal/is-closed|GET|200|400|json| Is terminal closed, not accepting or processing containers|
|<span style="color:green">Yes</span>|RPC|Q|root/terminal/status|GET|200|400|json| report current status terminal|
|<span style="color:green">Yes</span>|RPC|C|root/terminal/open|POST|200|400|empty| open a  closed terminal to accept an process containers|
|<span style="color:green">Yes</span>|RPC|C|root/terminal/close|POST|200|400|empty| closean open terminal. When closed it accepts no containers, and doesn't process them|
|<span style="color:green">Yes</span>|RPC|C|root/terminal/information|GET|200|400|json| report on id, version and capabiltiies of a terminal|
|<span style="color:yellow">No</span>|RPC|Q|root/terminal/holdingyard/information|GET|200|400|json| report on id, version  of a oldingyard|
|<span style="color:yellow">No</span>|RPC|Q|root/terminal/holdingyard/is-empty|GET|200|400|json| true if no containers are in the yard|
|<span style="color:yellow">No</span>|RPC|Q|root/terminal/holdingyard/is-filled|GET|200|400|json| true if yard is filled to capacity|
|<span style="color:yellow">No</span>|RPC|Q|root/terminal/holdingyard/capacity|GET|200|400|json| reports number of container the yard can hold|
|<span style="color:yellow">No</span>|RPC|Q|root/terminal/holdingyard/containers/count|GET|200|400|json| reports number of container in the yard|
|<span style="color:yellow">No</span>|RPC|Q|root/terminal/holdingyard/containers/{ID}|GET|200|400|json| reports number of container in the yard|






# Container
A container holds the ***Cargo*** to be processed inthe containers on its ***Routingslip***.
Cargo is always readable in a human form like a docker docker defnition file.
A container wihthout a routing slip doesn't know where to go.
A container contaiener has no knowledge of the terminal it is in.
## Sending a container

A ***Terminal*** can ***Send*** and ***Receive*** any ***Container***. 
A Terminal can only receive a contiainer when **Open**.
A ***failing*** Send means the container stays in the **HoldingYard**.
You can only Send to a specfic terminal as determinend from the ***Routingslip***.

# TripTracker
Every container has a  ***TripTracker*** attached. This is a device which registers the terminals  visteid, or the completed trips. It also determins the next  trip. If a  journey on the routingslip *ordered* it picks the next  one from the routingslip. Ifthe the next terminal is not availle for receiving, itmakes the container wait until it is. It then asks the currentterminla to forward the  container to the next terminal. If a trip is *not ordered* the Triptracker just  termines the next available  Receiving terminal and  asks the current terminal to forward the  container to this  terminal. 

# RoutingSip
A ***Routingslip*** describes all the terminals a contaitner has to visit.
A ***Trip*** is the moving from a container from one terminla to the next. 
A ***Journey*** is a ***possibly ordered*** **list of trips**.

# Trip
A Trip contains a Departure, Destination, Departure and Arrival time and an indication if the trip is realized.

# BlueTrain class diagram

```mermaid 

classDiagram

    class Terminal{
        +TerminalInformation Information
        +TerminalStatus status 
        +Capabilities Capabilities 
        +Open() void
        +Close() void
        +Receive( Container C ) bool 
        +Send( Terminal t, Container c) bool  
        +Remove( Container c) bool
        +IsOpen() bool 
        +IsProcessing() bool
        +IsReceiving() bool 
        +IsSending() bool
    }

    class Capabilities {
        
    }

    class Container {
        +Identfication identification  
        +TripTracker tripTracker
        +RoutingSlip routingSlip
        +Cargo cargo
    }

    class Cargo {
       
    }

    class HoldingYard {
        +int Capacity
        +IsEmpty() bool
        +IsFilled() bool
        +Add(Container c)   
        +Remove(Container c) Container  
        +FindByID(Guid ID) Container
    }
    
    class Identfication {
        +String Name
        +String Description
        +Guid ID 
        +DateTime CreatedOn
    }

    class RoutingSlip{
        +Created DateTime
        +Journey List~Trip~
    }

    class Trip{
        +TerminalInformation departure
        +TerminalInformation  destination
        +DateTine departedOn
        +DateTime arrivedOn
        +Completed() bool
    }

    class TripTracker{
        +TripTracker(Container c)
        +NextDestination( RoutingSlip rs ) Terminal
        +SetCompleted(Trip t)
        +ForwardContainer( Terminal nextTerminal, Container c)
        +JourneyStarted() bool
        +JourneyCompleted() bool
    }

    class ContainterHandler{
        +Handle( Container c ) Containter 
    }
    
    class TerminalInformation{
        +String Name 
        +String Description  
        +Guid Id
    }
    
    class TerminalStatus{
        << enumeration >>
        Closed
        Opening
        Open
        Closing
    }

    Container *-- TripTracker
    Container *-- RoutingSlip
    Container o-- Cargo
    Terminal *.. HoldingYard
    Terminal *..TerminalInformation
    Terminal *..ContainerHandler

```

    





