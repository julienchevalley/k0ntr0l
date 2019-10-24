namespace BangOnTekCommon
{
    public class PianoRollStep
    {
       public byte Note { get; set; }
       public bool IsRest { get; set; }
       public bool HasAccent { get; set; }
       public bool HasSlide { get; set; }
    }
}
