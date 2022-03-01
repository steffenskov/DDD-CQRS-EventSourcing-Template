# DDD-CQRS-EventSourcing-Template
Template project for showing how to implement the CQRS pattern with Event Sourcing in a Domain-Driven Design fashion with .Net 6.

Further Onion Architecture is applied as well, to enable easy replacement of infrastructure code without changes to the external API or Domain logic.

MediatR is used for the CQRS pattern as well as for persisting domain changes to both an eventsourced datasource as well as a snapshot source. Snapshots are optional when dealing with Event Sourcing, however they greatly speed up read access, so I strongly suggest going with snapshots.
Without snapshots you're bound to rehydrate aggregates over and over, leading to slower read performance as more domain events occur over the lifetime of your application.

The template application provides a CRUD API for dealing with todos and is split into 3 .Net projects:
- api
- core
- infrastructure

Finally a rudimentary grasp of the following concepts will be required to gain the most benefit from this template:
- [Domain-Driven Design](https://en.wikipedia.org/wiki/Domain-driven_design)
- [CQRS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation)
- [Event Sourcing](https://martinfowler.com/eaaDev/EventSourcing.html)
- [Onion Architecture](https://en.everybodywiki.com/Onion_Architecture)
- [MediatR](https://github.com/jbogard/MediatR/wiki)

## api
This project holds your WebAPI controller as well as any models sent and received to/from the user.
The controller mostly boils down to creating the proper Command or Query and sending it off using MediatR.

## core
Here's where all the magic happens. The project is sorted into 3 main folders:
- Exceptions holds any custom Exceptions we'll be using
- Interfaces holds all the generic interface definitions
- Todos holds everything related to our Todo aggregate

Beneath both Interfaces and Todos are a bunch of new folders, I'll describe them from the Todo perspective, as they're more concrete version of the generic ones beneat Interfaces:
- Aggregates holds the root aggregate (Todo) as well as any child aggregates (none in this simple template)
- Commands holds all Commands that can be sent using the CQRS pattern, these must be handled exactly once by the core domain
- Events holds all the Events that can be applied to a Todo, there's a 1:1 relationship between Commands and Events in this template, but this will not always be the case
- Notifications (only exists for Todo as it's rather simple) holds broadcast notifications that can be handled any number of times in both the core domain and potentially in supporting domains as well. In this template it's solemnly used for the PersistNotification which instructs both the eventsourced- and the snapshot datasource to persist a change.
- Queries holds all the Queries that can be sent using the CQRS pattern, these must be handled exactly once by the core domain
- Repositories are merely slightly more concrete interfaces for the eventsourced- and snapshot datasource.


## infrastructure
This is where your concrete infrastructure implementations go, in this template it's merely the eventsourced- and snapshot repositories. Dependency Injection is used in Setup.cs as a simple way for the API to inject the infrastructure. This is part of the Onion Architecture and allows you to easily swap out this layer.


# Understanding the template
I'd suggest reading the code in a top-down fashion starting with the API and following the code trail down into the core and finally infrastructure layer. This should give you a good understanding of both what is going on as well as *why*.