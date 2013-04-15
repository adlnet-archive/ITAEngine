#!/usr/bin/ruby

# Generate a text file of the unit-test results in copy/paste friendly
# manner.  This is more convienent for dropping into a document.

require "rexml/document"

filename = "TestResult.xml"

file = File.new(filename);
doc = REXML::Document.new(file);

doc.elements.each("test-results/test-suite/results/test-suite/results/test-suite/results/test-suite"){ |element|
	name = element.attributes["name"]
	puts "<h4>#{name}</h4>\n<p>"
	strip_test = "ITAEngine.Tests.#{name}."
	puts "<table>"
	puts "<tr><th>Test Name</th><th>Asserts</th><th>Description</th>"
	element.elements.each("results/test-case") { |testcase|
		name = testcase.attributes["name"].gsub!(strip_test, "")
		description = testcase.attributes["description"]
		asserts = testcase.attributes["asserts"]
		puts "<tr><td>#{name}</td><td align=\"center\">#{asserts}</td><td>#{description}</td>"
	}
	puts "</table></p>"
}

