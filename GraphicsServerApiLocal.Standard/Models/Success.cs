// <copyright file="Success.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIMatic.Core.Utilities.Converters;
using GraphicsServerApiLocal.Standard;
using GraphicsServerApiLocal.Standard.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace GraphicsServerApiLocal.Standard.Models
{
    /// <summary>
    /// Success.
    /// </summary>
    public class Success
    {
        [JsonExtensionData]
        private readonly IDictionary<string, JToken> additionalProperties;

        private readonly IEnumerable<string> propertyName;

        /// <summary>
        /// Get or set the value associated with the specified key in the AdditionalProperties dictionary.
        /// </summary>
        /// <param name="key">The key of the value to get or set. This must be a valid key that is not reserved for internal properties.</param>
        /// <returns>The object value associated with the specified key in the AdditionalProperties dictionary.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="key"/> is null or an empty string.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the specified <paramref name="key"/> conflicts with an internal property of the object.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when the specified <paramref name="key"/> does not exist in the AdditionalProperties dictionary.
        /// </exception>
        [IndexerName("AdditionalPropertiesIndexer")]
        public object this[string key]
        {
            get => additionalProperties.GetValue<object>(key);
            set => additionalProperties.SetValue(key, value, propertyName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success"/> class.
        /// </summary>
        public Success()
        {
            this.additionalProperties = new Dictionary<string, JToken>();
            this.propertyName = this.GetPropertyNames();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success"/> class.
        /// </summary>
        /// <param name="date">date.</param>
        /// <param name="temperatureC">temperatureC.</param>
        /// <param name="temperatureF">temperatureF.</param>
        /// <param name="summary">summary.</param>
        public Success(
            string date,
            int temperatureC,
            int temperatureF,
            string summary)
        {
            this.additionalProperties = new Dictionary<string, JToken>();
            this.propertyName = this.GetPropertyNames();
            this.Date = date;
            this.TemperatureC = temperatureC;
            this.TemperatureF = temperatureF;
            this.Summary = summary;
        }

        /// <summary>
        /// Gets or sets Date.
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets TemperatureC.
        /// </summary>
        [JsonProperty("temperatureC")]
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets or sets TemperatureF.
        /// </summary>
        [JsonProperty("temperatureF")]
        public int TemperatureF { get; set; }

        /// <summary>
        /// Gets or sets Summary.
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var toStringOutput = new List<string>();
            this.ToString(toStringOutput);
            return $"Success : ({string.Join(", ", toStringOutput)})";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is Success other &&
                (this.Date == null && other.Date == null ||
                 this.Date?.Equals(other.Date) == true) &&
                (this.TemperatureC.Equals(other.TemperatureC)) &&
                (this.TemperatureF.Equals(other.TemperatureF)) &&
                (this.Summary == null && other.Summary == null ||
                 this.Summary?.Equals(other.Summary) == true) &&
                (this.additionalProperties == null && other.additionalProperties == null ||
                 this.additionalProperties?.Count == other.additionalProperties?.Count &&
                 this.additionalProperties?.All(kv =>
                     other.additionalProperties.TryGetValue(kv.Key, out var value) &&
                     JToken.DeepEquals(kv.Value, value)) == true);
        }

        /// <summary>
        /// ToString overload.
        /// </summary>
        /// <param name="toStringOutput">List of strings.</param>
        protected void ToString(List<string> toStringOutput)
        {
            toStringOutput.Add($"Date = {this.Date ?? "null"}");
            toStringOutput.Add($"TemperatureC = {this.TemperatureC}");
            toStringOutput.Add($"TemperatureF = {this.TemperatureF}");
            toStringOutput.Add($"Summary = {this.Summary ?? "null"}");

            additionalProperties?
                .Select(kvp => $"[{kvp.Key}] = {kvp.Value.ToString(Formatting.None)}")
                .ToList()
                .ForEach(toStringOutput.Add);
        }
    }
}