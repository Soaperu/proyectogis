def decode(func):
    def decorator(*args):
        import base64
        response = func(*args)
        response = response.replace('aW5nZW1tZXQ', str())
        return base64.b64decode(response).decode('utf-8')

    return decorator


@decode
def connoragis():
    return 'ZGF0YV9lZGl0L0dJU19FRaW5nZW1tZXQElUQEJER0VPQ0FU'

@decode
def connorasys():
    return 'c2lzZ2VtL2aW5nZW1tZXQFsZG9yYUBvcmFjbGU='