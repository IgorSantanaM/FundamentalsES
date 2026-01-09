# Fundamentals of Event Sourcing

This repository contains a manual implementation of an **Event Sourcing (ES)** system. The goal is to demonstrate the core mechanics of the pattern without the abstraction of specialized frameworks, providing a "under-the-hood" look at how state is managed through events.

---

## 📖 What is Event Sourcing?

Event Sourcing is an architectural storage pattern where state changes are captured as a **sequence of immutable events**. 

In a traditional CRUD (Create, Read, Update, Delete) system, the database stores the *current state* of an object. When a change occurs, the old data is overwritten. In Event Sourcing, the **Event Store** becomes the "Single Source of Truth." We do not store the state; we store the history of how that state came to be.

### The "Memory is Cheap" Shift
Historically, developers focused on normalizing data and minimizing storage footprints because disk space was expensive. The phrase **"Memory is cheap"** (and by extension, storage) signaled a paradigm shift. Today, the business value of a 100% accurate audit trail and the ability to "Time Travel" through data far outweighs the cost of storing a high volume of events.

---

## 🛠️ Core Concepts

### 1. The Event Store
An append-only log of every change that has ever occurred. You never `UPDATE` or `DELETE` records in an Event Store. 

### 2. State Reconstruction (Rehydration)
To find the current state of an object (an **Aggregate**), the system:
1. Starts with a blank initial state.
2. Fetches all events related to that specific ID.
3. "Plays" or applies those events in chronological order.

### 3. Immutability
Once an event is written, it is a historical fact. It cannot be altered. If a mistake was made (e.g., an incorrect bank deposit), you do not delete the event; you issue a **Compensating Event** (a withdrawal) to correct the state.

---

## 🚀 Why Use Event Sourcing?

| Feature | Benefit |
| :--- | :--- |
| **Auditability** | Every change is tracked by default. Perfect for legal and financial compliance. |
| **Temporal Query** | You can view what the system looked like at any exact second in the past. |
| **Scalability** | Since the event store is append-only, it avoids many of the locking issues found in traditional RDBMS. |
| **Analytics** | You can create new "Read Models" (Projections) years after the data was collected to gain new business insights. |

---

## 📂 Repository Content

This project demonstrates the manual implementation of:
* **Event Store Schema:** How to structure your event table.
* **Aggregate Root:** The logic for applying events to rebuild state.
* **Event Dispatcher:** Managing the flow of events from commands to persistence.
* **Projections:** Turning a stream of events into a readable database table (CQRS).

---

## 🚦 Getting Started

> [!NOTE]  
> This implementation is designed for educational purposes to show the logic behind the pattern.

1. Clone the repo.
2. Run the initial setup: `dotnet build` & `dotnet run`
