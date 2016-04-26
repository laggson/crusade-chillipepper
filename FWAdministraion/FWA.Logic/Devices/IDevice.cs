namespace FWA.Logic.Devices
{
    interface IDevice
    {
        string Location
        {
            get; set;
        }

        string Name
        {
            get; set;
        }

        string InvNumber
        {
            get; set;
        }

        bool Checkkard
        {
            get; set;
        }

        short Amount
        {
            get; set;
        }

        short AnnualChecks
        {
            get; set;
        }

        CheckType Type
        {
            get; set;
        }
    }

    enum CheckType
    {
        NotNeeded,
        NotYetChecked,
        OK,
        Lacks,
        Repaired,
    }
}
