-- Create databases
CREATE DATABASE catalog;
CREATE DATABASE basket;
CREATE DATABASE "order";

-- Create users and assign passwords
CREATE USER catalog WITH ENCRYPTED PASSWORD 'C4tal0g';
CREATE USER basket WITH ENCRYPTED PASSWORD 'B4sk3t';
CREATE USER "order" WITH ENCRYPTED PASSWORD '0rd3r';

-- Grant privileges to users on their respective databases
GRANT ALL PRIVILEGES ON DATABASE catalog TO catalog;
GRANT ALL PRIVILEGES ON DATABASE basket TO basket;
GRANT ALL PRIVILEGES ON DATABASE "order" TO "order";
