# sb

sb, Simple Backup, is a tool to be able to manage your backups efficiently. Here is the documentation for the currently available commands. This is just the start of a side-project in which I plan to get the community involved in.

## Commands

### Command: `sb config`

#### Description: Allows the configuration of sb to be modified through different settings.

### Syntax

```bash
sb config [setting] [value]
```

### Settings:
<<<<<<< Updated upstream
| Option | Description | Example Value | Default Value Windows | Default Value Linux | All Values |
|-|-|-|-|-|-|
| `auto-compression` | Sets AutoCompression to a bool. It automatically compresses to AutoCompressionType if True. | `true` | `true` | `true` | `true` or `false` |
| `auto-compression-type` | Sets the AutoCompressionType to a CompressionTypes Enum. Sets the type of compression file used for AutoCompression. | `zip` | `zip` | `zip` | `zip`, `gz`, `lz`, `tar`, `sz (7z)`, `rar`, `cab`, `iso`, `targz`, `tarbzt (tar.bz2)`,`tarxz`, `bzt (bz2)`, `deb`, `rpm` |
| `auto-delete-backups` | Sets AutoDeleteBackups to a bool. Determines if the program periodically deletes previous backups. | `false` | `true` | `true` |`true` or `false` |
| `backup-time` | Sets the time for backups to occur during the day. Allows 12 and 24 hour time using DateTime for formatting. Using the HH:mm format. | `02:00`  | `02:00` | `02:00` | Anytime in that format (24 hr time)
| `destination-path` | Sets the DestinationPath to a file path where the backups will be stored. | `C:\backups\` | `C:\Users\{user}\AppData\Roaming\sb\backups` | `/home/{user}/.sb/backups` | Any folder that exists on the machine | 
| `enable-auto-verification` | Allows automatic verification of backups files after completion. | `true` | `false` | `false` | `true` or `false` |
| `enable-encryption` | Allows encryption to be set on newly created backup files. | `true` | `false` | `false` | `true` or `false` |
| `encryption-key-location` | Sets the path of the encryption key for the backup files encryption. This never be used if the encryption key is stored on the same machine that the program is running on. There will be a different way of handling the key in the future. | `C:\keys` | `C:\Users\{user}\AppData\Roaming\sb\keys\` | `/home/{user}/.sb/keys/` | Any folder that exists on the machine. |
| `incremental-compare-method` | Sets the compare method for seeing if the files have changed and need a backup. | `timestamp` | `timestamp` | `timestamp` | `timestamp` or `checksum` |
| `log-file-path` | Sets the file path to where all logs will be written. | `C:\logs` | `C:\Users\{user}\AppData\sb\logs` | `/home/{user}/.sb/logs` | Any folder that exists on the machine. |
| `log` | Sets whether or not the program should log. | `true` | `true` | `true` | `true` or `false` |
| `restore-directory` | Sets the file path of where the restored backups will go. | `C:\restored` | `C:\Users\{user}\AppData\sb\restored` | `/home/{user}/.sb/restored` | Any folder that exists on the machine. | 
| `restore-overwrite-behavior` | Sets a boolean for whether to overwrite the existing files in a restored backup. | `false` | `true` | `true` | `true` or `false` |
| `retention-length` | Sets the length of time to keep the backups before deletion in days. | `30` | `30` | `30` | Any number of days as a integer |
| `schedule` | Sets the schedule on when to do backups. The is in combination with backup-time. | `daily` | `weekly` | `weekly` | `daily`, `weekly`, `fortnightly`, `monthly`, `yearly` |
| `scheduler` | Sets the scheduler type to manage scheduled backups. | `cron` | `taskscheduler` | `cron` | `cron` or `taskscheduler` |
| `use-incremental` | Sets the program to use increments for backups. | `false` | `false` | `false` | `true` or `false` |
| `verification-method` | Sets the verification method used to verify backup files. | `checksum` | `checksum` | `checksum` | `checksum` or `timestamp` |
=======

| Option                         | Description                                                                                                                                                                                                                                  | Example Value   | Default Value Windows                          | Default Value Linux           | All Values                                                                                                                                           |
| ------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------- | ---------------------------------------------- | ----------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- |
| `auto-compression`           | Sets AutoCompression to a bool. It automatically compresses to AutoCompressionType if True.                                                                                                                                                  | `true`        | `true`                                       | `true`                      | `true` or `false`                                                                                                                                |
| `auto-compression-type`      | Sets the AutoCompressionType to a CompressionTypes Enum. Sets the type of compression file used for AutoCompression.                                                                                                                         | `zip`         | `zip`                                        | `zip`                       | `zip`, `gz`, `lz`, `tar`, `sz (7z)`, `rar`, `cab`, `iso`, `targz`, `tarbzt (tar.bz2)`,`tarxz`, `bzt (bz2)`, `deb`, `rpm` |
| `auto-delete-backups`        | Sets AutoDeleteBackups to a bool. Determines if the program periodically deletes previous backups.                                                                                                                                           | `false`       | `true`                                       | `true`                      | `true` or `false`                                                                                                                                |
| `backup-time`                | Sets the time for backups to occur during the day. Allows 12 and 24 hour time using DateTime for formatting.                                                                                                                                 | `02:00`       | `02:00`                                      | `02:00`                     | Anytime in that format (24 hr time)                                                                                                                  |
| `destination-path`           | Sets the DestinationPath to a file path where the backups will be stored.                                                                                                                                                                    | `C:\backups\` | `C:\Users\{user}\AppData\Roaming\sb\backups` | `/home/{user}/.sb/backups`  | Any folder that exists on the machine                                                                                                                |
| `enable-auto-verification`   | Allows automatic verification of backups files after completion.                                                                                                                                                                             | `true`        | `false`                                      | `false`                     | `true` or `false`                                                                                                                                |
| `enable-encryption`          | Allows encryption to be set on newly created backup files.                                                                                                                                                                                   | `true`        | `false`                                      | `false`                     | `true` or `false`                                                                                                                                |
| `encryption-key-location`    | Sets the path of the encryption key for the backup files encryption. This never be used if the encryption key is stored on the same machine that the program is running on. There will be a different way of handling the key in the future. | `C:\keys`     | `C:\Users\{user}\AppData\Roaming\sb\keys\`   | `/home/{user}/.sb/keys/`    | Any folder that exists on the machine.                                                                                                               |
| `incremental-compare-method` | Sets the compare method for seeing if the files have changed and need a backup.                                                                                                                                                              | `timestamp`   | `timestamp`                                  | `timestamp`                 | `timestamp` or `checksum`                                                                                                                        |
| `log-file-path`              | Sets the file path to where all logs will be written.                                                                                                                                                                                        | `C:\logs`     | `C:\Users\{user}\AppData\sb\logs`            | `/home/{user}/.sb/logs`     | Any folder that exists on the machine.                                                                                                               |
| `log`                        | Sets whether or not the program should log.                                                                                                                                                                                                  | `true`        | `true`                                       | `true`                      | `true` or `false`                                                                                                                                |
| `restore-directory`          | Sets the file path of where the restored backups will go.                                                                                                                                                                                    | `C:\restored` | `C:\Users\{user}\AppData\sb\restored`        | `/home/{user}/.sb/restored` | Any folder that exists on the machine.                                                                                                               |
| `restore-overwrite-behavior` | Sets a boolean for whether to overwrite the existing files in a restored backup.                                                                                                                                                             | `false`       | `true`                                       | `true`                      | `true` or `false`                                                                                                                                |
| `retention-length`           | Sets the length of time to keep the backups before deletion in days.                                                                                                                                                                         | `30`          | `30`                                         | `30`                        | Any number of days as a integer                                                                                                                      |
| `schedule`                   | Sets the schedule on when to do backups. The is in combination with backup-time.                                                                                                                                                             | `daily`       | `weekly`                                     | `weekly`                    | `daily`, `weekly`, `fortnightly`, `monthly`, `yearly`                                                                                      |
| `scheduler`                  | Sets the scheduler type to manage scheduled backups.                                                                                                                                                                                         | `cron`        | `taskscheduler`                              | `cron`                      | `cron` or `taskscheduler`                                                                                                                        |
| `use-incremental`            | Sets the program to use increments for backups.                                                                                                                                                                                              | `false`       | `false`                                      | `false`                     | `true` or `false`                                                                                                                                |
| `verification-method`        | Sets the verification method used to verify backup files.                                                                                                                                                                                    | `checksum`    | `checksum`                                   | `checksum`                  | `checksum` or `sizecomparison`                                                                                                                   |
>>>>>>> Stashed changes

## Running Debug Version of sb

First off, we need to clone the repository:

```sh
git clone https://github.com/Peggun/sb
```

Then open the solution `.sln` file in Visual Studio 2022, and build the solution using `Crtl-Shift-B` or selecting `Build Solution` in the Build Menu.

After this, open a terminal and navigate to the folder in which the solution is stored, and navigate to this directory

```sh
cd {currentDirectory}/sb.cli/bin/Debug/net8.0
```

After this you can run the command line tool and verifying if it works by running `sb.exe --version`.

Now you should be good to go! Have fun helping to develop sb!
