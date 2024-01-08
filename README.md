# Temper Demos

A repo for demos that present nicely.

So far, we just have a client/server data validation demo, but the intent is to
add other demos as well.

We've also made demo videos using the code here. Maybe we should link to those.

## Shell prompts

When recording demos, a nice shell prompt can help. Set this one in the root of
the demo, then cd to a different dir for relative path on the prompt:

```bash
unset PROMPT_COMMAND; PS1='\[\033[01;34m\]$(pwd | sed "s@'"$(pwd)"'/\?@@")\$\[\033[00m\] '
```
