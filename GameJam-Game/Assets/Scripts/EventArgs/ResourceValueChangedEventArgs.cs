namespace Nidavellir.EventArgs
{
    public class ResourceValueChangedEventArgs : System.EventArgs
    {
        public float NewValue { get; }
        
        public ResourceValueChangedEventArgs(float newValue)
        {
            this.NewValue = newValue;
        }
    }
}