<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<html>
			<head>
				<title>Book List</title>
			</head>
			<body>
				<h1>Books</h1>
				<table border="1">
					<tr>
						<th>Title</th>
						<th>Author</th>
						<th>Year</th>
					</tr>
					<xsl:for-each select="Books/Book">
						<tr>
							<td>
								<xsl:value-of select="Title" />
							</td>
							<td>
								<xsl:value-of select="Author" />
							</td>
							<td>
								<xsl:value-of select="Year" />
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>