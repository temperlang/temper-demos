# Temper Demos

A repo for demos that present nicely.

## Shell prompts

This setting in Linux can help for nice bash prompts, by the way:

```bash
unset PROMPT_COMMAND; PS1='\[\033[01;34m\]$(pwd | sed "s@'"$(pwd)"'/\?@@")\$\[\033[00m\] '
```

Set that in the root dir of a demo, then cd to some subdir for relative path on
the prompt.
