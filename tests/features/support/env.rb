require 'capybara/cucumber'
require 'selenium-webdriver'
require 'cucumber'
require 'site_prism'
require 'pry'
require 'faker'
require 'ostruct'
require 'rspec'

Capybara.default_driver = :selenium
Capybara.default_max_wait_time = 10
#Capybara.javascript_driver = :webkit

Capybara.register_driver :chrome do |app|
  Capybara::Selenium::Driver.new(app, :browser => :chrome)
end

Capybara.default_driver = :chrome