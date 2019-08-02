using AdventuresUnknownSDK.Core.Utils.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;
namespace AdventuresUnknown.Utils
{
    public class AdventuresUnknownFormatter : IFormatter
    {
        private SerializationBinder m_Binder;
        private StreamingContext m_Context;
        private ISurrogateSelector m_SurrogateSelector;
        #region Properties
        public SerializationBinder Binder { get => m_Binder; set => m_Binder = value; }
        public StreamingContext Context { get => m_Context; set => m_Context = value; }
        public ISurrogateSelector SurrogateSelector { get => m_SurrogateSelector; set => m_SurrogateSelector = value; }
        #endregion

        #region Methods
        public AdventuresUnknownFormatter()
        {
            m_Context = new StreamingContext(StreamingContextStates.All);
        }

        public object Deserialize(Stream serializationStream)
        {
            StreamReader sr = new StreamReader(serializationStream);
            JsonTextReader jsonTextReader = new JsonTextReader(sr);
            JObject jObject = JObject.Load(jsonTextReader);
            object obj = AdventuresUnknownSerializeUtils.DeserializeFull(jObject);
            sr.Close();
            return obj;
        }
        
        public void Serialize(Stream serializationStream, object graph)
        {
            StreamWriter sw = new StreamWriter(serializationStream);
            JsonTextWriter jsonTextWriter = new JsonTextWriter(sw);
            jsonTextWriter.Indentation = 2;
            jsonTextWriter.Formatting = Formatting.Indented;
            jsonTextWriter.IndentChar = ' ';
            AdventuresUnknownSerializeUtils.SerializeFull(jsonTextWriter, graph);
            sw.Close();
        }
        #endregion
    }
}
