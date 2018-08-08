#--- TERMINOLOGY ---#
1) TPL - Task Parallel Library
2) PLINQ - Parallel LINQ
3) Thread - each thread has it's own private stack. That means, that variables are kept seperated in memory.
4) Thread synchronization - controlling the execution of threads. For example set up that the first thread has to wait for the second thread, till he finishes it's execution And only after that start the execution of the first thread.
5) Race conditions - Generally: when a single block of code is being executed by more than one thread simultaneously. two or more Other case: two or more threads are accessing uncontrolled a single set of data (for example a counter variable). Program execution no longer follows a predictable path. 
	Code starts behaving in unpredictable ways. The problem can be fixed by locking threads. Golden rule: KEEP DATA YOU SHARE BETWEEN THREADS TO AN ABSOLUTE MINIMUM.
6) Locking - mechanism to safely share data between threads.
7) Tasks - workhorse of asynchronous programming. 
8) Critical section - a section of code that can not be interrupted by another thread. Thread locking means, that a thread is waiting for execution of code that is in a critical section, executed by another thread. 
	We should lock threads, when two or more threads are reading and writing the same shared varaible. It allows to eliminate 99% of all race conditions in your code.
9) Nested locks - see example 7 in THREADS folder.

#--- LOCKING ---#
- The Lock statement is syntactic sugar for a Monitor.Enter / Monitor.Exit pair and sets up a critical section.
- The Monitor class also has a TryEnter method that supports a Lock timeout value.
- Lock requires a reference type synchronisation object. You can use any object you like, but a unique private object field is recommended.
- You can nest lock statements. The critical section is unlocked, when you exit the outermost lock.

#--- THREAD SYNCHRONIZATION ---#
- If you want to safely pass data between two threads, you need to synchronise the threads.
- Synchronization can be created using AutoResetEvent. A call to WaitOne suspends a thread, and a call to Set resumes the thread.
- For a robust communication channel you need two AutoResetEvents with calls to WaitOne and Set at both ends.

#--- TASKS ---#
- The TPL provides a Task class for asynchronously performing a unit of work and returning the result to another thread.
- You access the Result property when you need the result. The property blocks it the result is not yet available.
- Tasks are lightweight abstractions of an asynchronous unit of work. You can safely create hundreds or thousands of tasks.
- When a task has been created with TaskCreationOptions.LongRunning it's being dettached from the thread pool.
- Use the Task class for tasks that do net return a result. The Wait method blocks until the task has completed.
- Use the Task<T> class for tasjs that do return a result. The Result property blocks until the task has completed.
- Tasks execute on the .NET runtime thread pool. For long-running and I/O bound tasks you can probide the LongRunning option to execute the on a non-pool thread
- Any exceptions thrown by a task propagate to the calling code and are automatically re-thrown in the Wait method and Result property.
- Tasks can be initialised with either a startup object or by capturing variables in their lambda expression.
- Visual Studio displays the AsyncState task property in the Parallel Tasks window. If you store a meaningful task name, here it will greatly aid debugging.
- Tasks can be cancelled with a cancellation token.
- Task continuations - sequential work of multiple tasks (one is being run after the previous completed).

#--- Parallel work ---#
- Split a problem into many units of work that can execute in parallel.
- Execute the units of work.
- Assemble the finished work into a result.
- The common name for this process is MapReduce.

#--- PLINQ ---#
- PLINQ automatically creates the network of tasks to execute the PLINQ expression.
- You have no imperative control overr the task network.
- You can only declare the sequence of operations to execute on the items in the dataset.
- Use PLINQ if you only need to performa a sequence of operations on the items in a large dataset.
- PLINQ does not preserve the order of the items in the dataset.
- Operations Concat, First, FirstOrDefault, Last, LastOrDefault, Skip, SkipWhile, Take, TakeWhile, Zip are in .NET 4 sometimes sequential (.NET 4.5 and above always parallel)
- Positional operations Select, SelectMany, SkipWhile, TakeWhile, Where are sometimes sequential also in all .NET version.
- Please visit https://blogs.msdn.microsoft.com/pfxteam/2011/11/10/plinq-queries-that-run-in-parallel-in-net-4-5/
- Please visit https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/understanding-speedup-in-plinq

#--- Solutions and their usages ---#
- Tasks - Complex process + small shared data
- Parallel class - Complex process + large shared data
- Tasks OR PLINQ - Sequence of tasks on items + small dataset
- PLINQ - sequence of tasks on items + large dataset