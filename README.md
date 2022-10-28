# adb2tgz
A small utility application to convert android adb backup to tgz archive which can be opened with 7zip or any other zip utilities.


# How to use
Download the application from release section on right side bar of this page.
Drag your adb file on top of `adb2tgz.exe` or use following command in command line.
    adb2tgz.exe {path to your adb backup file}

Once the process is complete adb file will be renamed to tgz.

## Note:

Due to normally large size of adb backup files, this utility will not create a second file and will modify original file.
Running this utility on your adb more than once might corrupt it, Make a backup from your adb file before using this.


