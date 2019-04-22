namespace Mst.Dexter.Configuraton
{
    using System.Configuration;
    using System.Xml;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dx external configuration section handler. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class DxExternalConfigurationSectionHandler : IConfigurationSectionHandler
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public DxExternalConfigurationSectionHandler()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a configuration section handler. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="parent">           Parent object. </param>
        /// <param name="configContext">    Configuration context object. </param>
        /// <param name="section">          Section XML node. </param>
        ///
        /// <returns>   The created section handler object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public object Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}
