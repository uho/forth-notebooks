\ gforth-serverclient.fs
\ author: uho@xlerb.de
\
\ This is part of the gforth integration with IPython notebook
\
\ This script implements a gforth server program that communicates with 
\ a client via pipes "forth-in" and "forth-out". 

\ start this gforth server by issuing the command
\
\   gforth gforth-server.fs -e "start"

\ patch all output to go via outfile

: becomes  ( <name> new-xt -- ) \ makes <name> behave as new-xt  
   here >r  
   >r   ' >body dp !  
   postpone ahead   r> >body dp !   postpone THEN  
   r> dp !  
; 

:noname ; becomes >stderr 

: forth-in ( -- addr len ) s" forth-in" ;   \ named pipe for client->server communication: mkfifo forth-in
: forth-out ( -- addr len ) s" forth-out" ; \ named pipe for server->client communication: mkfifo forth-out

: server ( -- )
    ." gforth server ready" cr  
    BEGIN
       forth-in r/o open-file 
       ?dup 0= 
    WHILE ( )
       forth-out w/o open-file throw to outfile-id 
       ['] include-file catch ?dup IF <# DoError clearstacks ELSE ."  ok" cr outfile-id flush-file throw THEN
       outfile-id  stdout to outfile-id   close-file throw
    REPEAT
    ." gforth server died with " . bye ;

: start ( -- ) server ;
