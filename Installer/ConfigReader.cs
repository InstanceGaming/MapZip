using System;

namespace mapzip
{
    class ConfigReader
    {
        private string m_RawText = "";
        private ConfigStructure m_Structure;

        public ConfigStructure Config { get { return m_Structure; } }

        public ConfigReader(string raw)
        {
            m_RawText = raw;
        }

        public void Parse()
        {
            if (String.IsNullOrWhiteSpace(m_RawText))
            {
                Logger.Out(Properties.Settings.Default.Text_ConfEmpty,0,Utils.TextTags[1]);
            }
            m_Structure = JsonInterface.DeserializeToStructure<ConfigStructure>(m_RawText);
        }
    }
}
