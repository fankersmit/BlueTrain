namespace BlueTrain.Shared
{
    public class Description : TinyTypeOfString
    {
        // ctor
        public Description(string description) : base ( 255, "No description", description)
        {
        }
    }
}
