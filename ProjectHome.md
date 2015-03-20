# File Vader #

## Utility to clean up/free up space in a folder and its subfolders. ##

  * Deleting files or folders after a certain period of time.
  * Deleting files with certain extension.
  * Supports files over 1GB
  * Supports UNC file paths

### Overview: ###

This software frees up a disk space by removing files older than specified number of days. It supports UNC paths and large files. Further versions will support more options.

### Prerequisites: ###

.NET framework 3.5

### Configuration: ###

Software requires 3 parameters

- scan

- ext

- log

-days


### Examples: ###

FileVader -scan "D:\SQLBackup" -ext bak -log "D:\Logs\" –days 2

This command removes all files with extension bak that are found in directory D:\SQLBackup that are older than 2 days

To execute over the network use:

**\\appserver\Tools\FileVader -scan “\\appserver\SQLBackup” -ext bak -log  \\appserver\Logs -days 0**

Here option –days 0 removes all **.bak files.**

**Logs:**

> 22/06/2010 02:56:43 INFO: Found 1 files with extension bak

> 22/06/2010 02:56:43 INFO: Removing files older than 0 (22/06/2010).

> 22/06/2010 02:56:43 INFO: File removed: \\appserver\SQLBackup\Default\_audit\asdasd.bak_

> 22/06/2010 02:56:43 INFO: Removed 1 files

> 22/06/2010 02:57:23 INFO: Found 1 files with extension bak

> 22/06/2010 02:57:23 INFO: Removing files older than 0 (22/06/2010).

> 22/06/2010 02:57:23 INFO: File removed: \\appserver\SQLBackup\Default\_audit\New Text Document.bak_

> 22/06/2010 02:57:23 INFO: Removed 1 files