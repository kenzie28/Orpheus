gcc single-server.c -o serve `pkg-config --cflags --libs gstreamer-1.0` -lgstrtspserver-1.0
gcc gserver.cpp -o device-scanner `pkg-config --cflags --libs gstreamer-1.0` -lgstrtspserver-1.0