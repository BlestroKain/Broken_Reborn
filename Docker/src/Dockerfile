FROM mono:latest

ENV HOME /root
ENV USER root
ENV APP_DIR $HOME/intersect-server

USER $USER

# this needs to be changed for automated builds
ADD ./server $APP_DIR

WORKDIR $APP_DIR

RUN set -eux \
    && chown -R "${USER}:${USER}" "${APP_DIR}"

VOLUME ${APP_DIR}/resources/
VOLUME ${APP_DIR}/logs/

ENV LD_LIBRARY_PATH=.

CMD ["/usr/bin/mono", "/root/intersect-server/Intersect Server.exe"]