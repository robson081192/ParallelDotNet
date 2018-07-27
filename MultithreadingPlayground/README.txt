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