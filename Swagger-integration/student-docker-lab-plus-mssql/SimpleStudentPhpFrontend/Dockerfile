# PHP 8.3 + Apache. No Composer packages and no extra extensions are needed.
FROM php:8.3-apache

# Only public/ is the web root; lib/, views/ and openapi/ are not reachable from the browser.
COPY public/  /var/www/html/
COPY lib/     /var/www/lib/
COPY views/   /var/www/views/
COPY openapi/ /var/www/openapi/

# COPY keeps the file permissions from your computer. If the files are readable only by
# their owner (e.g. -rw-------), Apache (user www-data) gets "Permission denied".
# Make everything readable for the web server, independent of the host permissions.
RUN find /var/www -type d -exec chmod 755 {} + \
 && find /var/www -type f -exec chmod 644 {} +

EXPOSE 80
