namespace _1.ATM_Info.Core.Model
{
    class Phone
    {
        public string Text { get; set; }
        public PhoneType Type { get; set; }

        public override string ToString()
        {
            return Type.ToString() + ": " + Text;
        }
    }
}
