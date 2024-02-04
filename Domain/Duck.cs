namespace HttpClientApp.Domain
{
  internal class Duck : Animal
  {
    public Duck( string name, string color, Uri image ) : base( name, color, image )
    {
      this.Name = name;
      this.Color = color;
      this.Sound = "Quack";
      this.Image = image;
    }

    public override void Speak()
    {
      Console.WriteLine( $"{Name} says {Sound}" );
    }
  }
}
