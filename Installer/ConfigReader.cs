using System;

namespace mapzip
{
    class ConfigReader
    {
        private string m_RawText = "";
        private ConfigStructure m_Structure;

        public ConfigStructure Config { get { return m_Structure; } }

        /// <summary>
        /// Create a new configuration reader
        /// </summary>
        /// <param name="raw">the raw file text</param>
        public ConfigReader(string raw)
        {
            m_RawText = raw;
        }

        /// <summary>
        /// Serialize the configuration into a ConfigStructure object
        /// </summary>
        public void Parse()
        {
            m_Structure = JsonInterface.DeserializeToStructure<ConfigStructure>(m_RawText);
        }
    }
}
