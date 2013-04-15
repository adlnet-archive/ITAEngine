#!/usr/bin/ruby
#
#

require "rexml/document"

filename = ARGV[0]

file = File.new(filename);
doc = REXML::Document.new(file);

doc.elements.each("test") { |element|
	name = element.attributes["name"]
	puts "<h4>#{name}</h4>"
}

doc.elements.each("test/description") { |element|
	puts "<p>#{element.text}</p>"
}

doc.elements.each("test/data_description") { |element|
	puts "<h5>Data Description</h5> <p>#{element.text}</p>"
}

	puts "<h5>Procedure</h5><p><table>"
	puts "<tr><th>Actions</th><th>API Calls</th></tr>"
        doc.elements.each("test/procedures/item") { |item|
		action = ""
		api = ""
		observe = ""
		api2 = ""
		item.elements.each { |subs|
			element = subs.name
			if element == "action"
				action = subs.text
			elsif element == "api" && observe == ""
				api = subs.text
			elsif element == "observe"
				observe =  subs.text
			elsif element = "api" &&  observe != ""
				api2 = subs.text
			end	
			}
		puts "<tr>\n\t<td>#{action}</td> <td>#{api}</td>\n</tr>"
		if observe != ""
			puts "   <tr>\n\t<td>Observe: #{observe}</td> <td>#{api2}</td>\n</tr>"
		end
        }
	puts "</table></p>"


