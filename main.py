import pandas as pd


class Node:

    def __init__(self, n, data):
        self.children = [None] * n
        self.data = data


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
    for key in panda_json(json_file):
        print(f"{key}")

        value = panda_json(json_file)[key]
        print(value)
