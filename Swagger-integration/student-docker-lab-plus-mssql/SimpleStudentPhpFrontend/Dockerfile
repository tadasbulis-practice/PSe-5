# PHP 8.3 + Apache. No Composer packages and no extra extensions are needed.
FROM php:8.3-apache

# Only public/ is the web root; lib/, views/ and openapi/ are not reachable from the browser.
COPY public/  /var/www/html/
COPY lib/     /var/www/lib/
COPY views/   /var/www/views/
COPY openapi/ /var/www/openapi/

EXPOSE 80
