using net.liberty_development.SaxonHE11s9apiExtensions;
using net.sf.saxon.s9api;
using System.Reflection;

// force loading of updated xmlresolver
ikvm.runtime.Startup.addBootClassPathAssembly(Assembly.Load("org.xmlresolver.xmlresolver"));
ikvm.runtime.Startup.addBootClassPathAssembly(Assembly.Load("org.xmlresolver.xmlresolver_data"));

string xslt = $$"""
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  version="3.0"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  exclude-result-prefixes="#all"
  expand-text="yes">

  <xsl:output indent="yes"/>

  <xsl:template match="/" name="xsl:initial-template">
    <test>Run at {current-dateTime()} with {system-property('xsl:product-name')} {system-property('xsl:product-version')} {system-property('Q{http://saxon.sf.net/}platform')}</test>
    <xsl:comment>Run at {current-dateTime()} with {system-property('xsl:product-name')} {system-property('xsl:product-version')} {system-property('Q{http://saxon.sf.net/}platform')}</xsl:comment>
  </xsl:template>

</xsl:stylesheet>
""";

var processor = new Processor(false);

var xslt30Transformer = processor.newXsltCompiler().compile(xslt.AsSource()).load30();

xslt30Transformer.callTemplate(null, processor.NewSerializer(Console.Out));
