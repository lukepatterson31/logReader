import json

import pandas as pd


class Node:

    def __init__(self, n, value):
        self.children = [None] * n
        self.value = value


def in_order(self, node):
    if node is None:
        return

    total = len(node.children)

    # All the children except the last
    for i in range(total - 1):
        self.in_order(node.children[i])

    print(node.data, end=" ")

    # Last child
    self.in_order(node.children[total - 1])


def panda_json(file):
    file = pd.read_json(file)
    log_message = file["log"]["Message"]
    return log_message


json_file = "data/sample-corr.json"


if __name__ == '__main__':
    df = pd.read_json("data/sample-corr.json")
    with open(json_file, 'r') as f:
        data = json.loads(f.read())
        df_nested = pd.json_normalize(data, max_level=7)
    # print(df.info())
