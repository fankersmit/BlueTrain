namespace Station.Domain
{
   public class Name : TinyTypeOfString
   {
       // properties
        
       // ctor
       public Name(string name) : base( 25, "Default Station", name ) 
       {
       }
    }
}