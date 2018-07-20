#--- TERMINOLOGY ---#
1) TPL - Task Parallel Library
2) PLINQ - Parallel LINQ
3) Thread - each thread has it's own private stack. That means, that variables are kept seperated in memory.
4) Thread synchronization - controlling the execution of threads. For example set up that the first thread has to wait for the second thread, till he finishes it's execution And only after that start the execution of the first thread.
5) Race conditions - Generally: when a single block of code is being executed by more than one thread simultaneously. two or more Other case: two or more threads are accessing uncontrolled a single set of data (for example a counter variable). Program execution no longer follows a predictable path. 
	Code starts behaving in unpredictable ways. The problem can be fixed by locking threads. Golden rule: KEEP DATA YOU SHARE BETWEEN THREADS TO AN ABSOLUTE MINIMUM.
6) Locking - mechanism to safely share data between threads.
7) Tasks - workhorse of asynchronous programming. 
 