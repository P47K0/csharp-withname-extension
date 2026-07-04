# csharp-withname-extension

A small C# utility for naming async tasks while debugging.

## Why

This extension was used to trace async flow in a worker that accepted TCP clients and started message handlers. By attaching a name to a task, it became easier to see when `GetAwaiter`, `OnCompleted`, and `GetResult` were reached in simple console output.

## Example

```csharp
await ReceiveMessagesAsync()
    .WithName("ReceiveMessagesAsync");
```

Example output:

```text
++++++++++++++++++++ GetAwaiter called for task "ReceiveMessagesAsync"
++++++++++++++++++++ OnCompleted called for task "ReceiveMessagesAsync"
++++++++++++++++++++ GetResult called for task "ReceiveMessagesAsync"
```

## Notes

- Lightweight and dependency-free.
- Uses `Console.WriteLine()` because it was originally meant for temporary local debugging.
- Inspired by Jon Skeet's task-naming/debugging ideas, with a custom implementation.
- Used this in 2022 to pin point a memory leak

## Use case

Useful when debugging async behavior, long-running tasks, or suspected leaks caused by handlers that are not completing or being cancelled.
