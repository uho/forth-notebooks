# forth-client.py 
# author: uho@xlerb.de
#
# This is part of the gforth integration with IPython notebook
#
# This script implements a client program that communicates with a running
# gforth instance via pipes "forth-in" and "forth-out" and via
# stdin and stdout with the IPython kernel.
#
# This script is intended to be called by IPython cell magic 
# such as %%gforth

# start the gforth instance by issuing the command
# > gforth gforth-server.fs -e "start"

import sys

# read in rest of IPython cell form stdin and push it to the forth-in pipe
forth_in=open("forth-in","wb")
for l in sys.stdin.readlines(): 
    forth_in.write(l+"\n")
    forth_in.flush()
forth_in.close()

# read the forth-out pipe and push it to line by line to stdout
forth_out=open("forth-out","rb")
for l in forth_out.read().split("\n"):
    print l
