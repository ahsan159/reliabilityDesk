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
			<body>
				<h2>Reliability Report</h2>
				<table>
					<tr bgcolor="#9acd32">
						<th>Part</th>
						<th>MTBF (hrs)</th>
						<th>Reliability</th>
						<th>UniqueID</th>
					</tr>
					<xsl:for-each select="/">
						<tr>
							<td>
								<xsl:value-of select="Project/@Name"/>
							</td>
							<td>
								<xsl:value-of select="Project/@MTBF"/>
							</td>
							<td>
								<xsl:value-of select="Project/@Reliability"/>
							</td>
						</tr>
						<!--<xsl:apply-templates></xsl:apply-templates>-->
					</xsl:for-each>
					<xsl:for-each select="*">
						<xsl:apply-templates/>
					</xsl:for-each>

				</table>
			</body>
		</html>
	</xsl:template>

	<xsl:template match="Assembly|Part|Project">
		<tr>
			<xsl:variable name="nodeLevel" select="count(ancestor::*)"/>			
			<xsl:call-template name="increment">
				<xsl:with-param name="index" select="$nodeLevel"></xsl:with-param>
			</xsl:call-template>
			<!--<td>
				<xsl:value-of select="$nodeLevel"/>
			</td>-->
			<td>
				<xsl:value-of select="@Name"/>
			</td>
			<td>
				<xsl:value-of select="@MTBF"/>
			</td>
			<td>
				<xsl:value-of select="@Reliability"/>
			</td>
			<!--<td>
				<xsl:value-of select="@id"/>
			</td>-->

			<xsl:if test="count(*)>0">
				<xsl:apply-templates></xsl:apply-templates>
			</xsl:if>

		</tr>
	</xsl:template>

	<xsl:template name="increment">
		<xsl:param name="index"></xsl:param>
		<xsl:if test="$index>0">
			<td>
				<!--<xsl:value-of select="$index"/>-->
				<xsl:text>→</xsl:text>
			</td>
			<xsl:call-template name="increment">
				<xsl:with-param name="index" select="$index - 1"></xsl:with-param>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>
