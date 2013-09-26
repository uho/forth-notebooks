#!/bin/sh

if [ ! -p forth-in ] 
then
  mkfifo forth-in
fi

if [ ! -p forth-out ] 
then
  mkfifo forth-out
fi


