namespace HttpClientApp.Domain
{
  internal class Dog : Animal
  {
    public Dog( string name, string color, Uri image ) : base( name, color, image )
    {
      this.Name = name;
      this.Color = color;
      this.Sound = "Woof";
      this.Image = image;
    }

    public override void Speak()
    {
      Console.WriteLine( $"{Name} says {Sound}" );
    }
  }
}
