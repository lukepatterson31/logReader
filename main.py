import pandas as pd

def panda_json(file):
    file = pd.read_json(file)
    log_message = file["log"]["Message"]
    return log_message

json_file = "data/sample-corr.json"

if __name__ == '__main__':
    for key in panda_json(json_file):
        print(f"{key}")
        value = panda_json(json_file)[key]
        print(value)

