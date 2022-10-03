using System;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class ResourceController
    {
        private EventHandler<ResourceValueChangedEventArgs> m_resourceValueChanged;
        private EventHandler<ResourceValueChangedEventArgs> m_maximumValueChanged;
        
        public float StartingValue { get; private set; }
        public float CurrentValue { get; private set; }
        public float MaxValue { get; private set; }

        public ResourceController(ResourceData resourceData)
        {
            if (resourceData.StartingValue > resourceData.MaxValue)
                throw new ArgumentException($"startingValue {resourceData.StartingValue} is greater than maxValue {resourceData.MaxValue}");

            this.StartingValue = resourceData.StartingValue;
            this.CurrentValue = resourceData.StartingValue;
            this.MaxValue = resourceData.MaxValue;
        }

        public ResourceController(float startingValue, float maxValue)
        {
            if (startingValue > maxValue)
                throw new ArgumentException($"startingValue {startingValue} is greater than maxValue {maxValue}");

            this.StartingValue = startingValue;
            this.CurrentValue = startingValue;
            this.MaxValue = maxValue;
        }
        
        public void Add(float value)
        {
            if (value < 0)
                throw new ArgumentException($"{value} is less than 0");

            var oldValue = this.CurrentValue;
            this.CurrentValue += value;
            this.m_resourceValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(this.CurrentValue, oldValue));
        }

        public bool CanAfford(float amount)
        {
            if (amount < 0)
                throw new ArgumentException($"{amount} is less than 0");

            return amount <= this.CurrentValue;
        }

        public void ResetValues()
        {
            var oldValue = this.CurrentValue;
            this.CurrentValue = 0f;
            this.m_resourceValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(this.CurrentValue, oldValue));
        }

        public void ResetToStartValues()
        {
            var oldValue = this.CurrentValue;
            this.CurrentValue = StartingValue;
            this.m_resourceValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(this.CurrentValue, oldValue));
        }

        public void UseResource(float amount)
        {
            if (amount < 0)
                throw new ArgumentException($"{amount} is less than 0");

            if (amount > this.CurrentValue)
                throw new InvalidOperationException($"{amount} is greater than {this.CurrentValue}");

            var oldValue = this.CurrentValue;
            this.CurrentValue -= amount;
            this.m_resourceValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(this.CurrentValue, oldValue));
        }

        public void SubtractResource(float amount)
        {
            if (amount < 0)
                throw new ArgumentException($"{amount} is less than 0");
            
            var oldValue = this.CurrentValue;
            this.CurrentValue -= amount;
            this.m_resourceValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(this.CurrentValue, oldValue));
        }

        public void ApplyDeltaToMaximumValue(float amount)
        {
            var oldValue = this.CurrentValue;
            var oldMaximumValue = this.MaxValue;
            this.MaxValue += amount;
            this.MaxValue = Math.Max(0, this.MaxValue);
            this.CurrentValue = Mathf.Min(this.CurrentValue, this.MaxValue);
            this.m_maximumValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(this.MaxValue, oldMaximumValue));
            this.m_resourceValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(this.CurrentValue, oldValue));
        }

        public event EventHandler<ResourceValueChangedEventArgs> ValueChanged
        {
            add => this.m_resourceValueChanged += value;
            remove => this.m_resourceValueChanged -= value;
        }    
    
        public event EventHandler<ResourceValueChangedEventArgs> MaxValueChanged
        {
            add => this.m_maximumValueChanged += value;
            remove => this.m_maximumValueChanged -= value;
        }
    }
}