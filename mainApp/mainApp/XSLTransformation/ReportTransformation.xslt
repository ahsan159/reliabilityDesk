<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes"/>

  <!--<xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>-->
  <xsl:template match="/">
    <html>
      <head>

        <style>
          table, th, td {
          border: 1px solid black;
          border-collapse: collapse;
          }
        </style>
      </head>
      <body>
        <h2>Reliability Report</h2>
        <table borderthickness="2">
          <tr>
            <th></th>
            <th>Name</th>
            <th>Reliability</th>
            <th>MTBF</th>
          </tr>
          <!--<tr>
            <xsl:for-each select="Project">
              <td>
                <xsl:value-of select="@Name"/>
              </td>
              <td>
                <xsl:value-of select="@Reliability"/>
              </td>
              <td>
                <xsl:value-of select="@MTBF"/>
              </td>
            </xsl:for-each>
          </tr>-->
          <xsl:apply-templates />
        </table>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="*">
    <tr>
      <xsl:variable name="level" select="count(ancestor::*)"/>
      <xsl:variable name="index" select="0"/>
      <xsl:call-template name="treeSpace">
        <xsl:with-param name="index" select="$index"></xsl:with-param>
        <xsl:with-param name="level" select="$level"></xsl:with-param>
      </xsl:call-template>
      <td>
        <xsl:value-of select="@Name"/>
      </td>
      <td>
        <xsl:value-of select="format-number(@Reliability,'#.00000')"/>
      </td>
      <td>
        <xsl:value-of select="format-number(@MTBF,'###,###,###')"/>
      </td>
    </tr>
    <xsl:if test="count(*)>0">
      <xsl:apply-templates/>
    </xsl:if>
  </xsl:template>

  <xsl:template name="treeSpace">
    <xsl:param name="index"/>
    <xsl:param name="level"/>
    <xsl:if test="$level>$index">
      <xsl:call-template name="treeSpace">
        <xsl:with-param name="index" select="$index + 1"></xsl:with-param>
        <xsl:with-param name="level" select="$level"></xsl:with-param>
      </xsl:call-template>
    </xsl:if>
    <td>
      <!--<xsl:value-of select="$index"/>-->
    </td>
  </xsl:template>
</xsl:stylesheet>
