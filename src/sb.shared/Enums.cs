﻿namespace sb.shared.enums
{
    public enum CompressionTypes
    {
        zip,
        gz,
        lz,
        tar,
        sv, // 7z
        rar,
        cab,
        iso,
        targz,
        tarbzt, // .tar.bz2
        tarxz,
        bzt, // .bz2
        deb,
        rpm
    }

    public enum ScheduleTimes
    {
        Daily,
        Weekly,
        Fortnightly,
        Monthly,
        Yearly
    }

    public enum Schedulers
    {
        cron,
        taskScheduler
    }

    public enum CompareMethods
    {
        Timestamp,
        Checksum
    }

    public enum VerificationMethods
    {
        Checksum,
        SizeComparison
    }

    public enum LogLevels
    {
        Info,
        Warning,
        Error,
        Debug
    }
}