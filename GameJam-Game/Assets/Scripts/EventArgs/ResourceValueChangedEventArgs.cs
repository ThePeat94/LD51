namespace Nidavellir.EventArgs
{
    public class ResourceValueChangedEventArgs : System.EventArgs
    {
        public float NewValue { get; }
        public float OldValue { get; }
        
        public ResourceValueChangedEventArgs(float newValue, float oldValue)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }
    }
}