namespace HttpClientApp.Domain
{
  internal abstract class Animal
  {
    public string Name { get; set; }
    public string Color { get; set; }
    public string Sound { get; set; }
    public Uri Image { get; set; }

    protected Animal( string name, string color, Uri image )
    {
      Name = name;
      Color = color;
      Image = image;
    }

    public override string ToString()
    {
      return $"{Name} is {Color} and says {Sound}";
    }

    public virtual void Speak()
    {
      Console.WriteLine( $"{Name} says {Sound}" );
    }
  }
}
