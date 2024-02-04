namespace HttpClientApp.Domain
{
  internal class Cat : Animal
  {
    public Cat( string name, string color, Uri image ) : base( name, color, image )
    {
      this.Name = name;
      this.Color = color;
      this.Sound = "Meow";
      this.Image = image;
    }

    public override void Speak()
    {
      Console.WriteLine( $"{Name} says {Sound}" );
    }
  }
}
