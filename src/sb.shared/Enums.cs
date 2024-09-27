namespace sb.shared.enums
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

    public enum CompareMethod
    {
        Timestamp,
        Checksum
    }

    public enum VerificationMethod
    {
        Checksum,
        SizeComparison
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Debug
    }
}