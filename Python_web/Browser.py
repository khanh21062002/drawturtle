import sys
from PyQt5 import QtCore, QtGui, QtWidgets, QtWebEngineWidgets

vocabulary = ['dog', 'cat', 'house', 'car', 'book']
grammar = ['noun', 'verb', 'adjective', 'adverb', 'preposition']
search = ['dictionary', 'youtube', 'spotify', 'instagram', 'oxfordlearnersdictionaries']

class WebBrowser(QtWidgets.QWidget):
    def __init__(self):
        super().__init__()

        # Create the QWebEngineView
        self.view = QtWebEngineWidgets.QWebEngineView(self)

        # Set the URL to a default value
        self.view.setUrl(QtCore.QUrl("https://www.google.com"))

        # Create the URL bar
        self.url_bar = QtWidgets.QLineEdit(self)
        self.url_bar.setPlaceholderText("Enter a URL or English word")

        # Connect the URL bar to the QWebEngineView's load function
        self.url_bar.returnPressed.connect(self.load_url)

        # Create the back, forward, and refresh buttons
        self.back_button = QtWidgets.QPushButton("Back", self)
        self.forward_button = QtWidgets.QPushButton("Forward", self)
        self.refresh_button = QtWidgets.QPushButton("Refresh", self)

        # Connect the back and forward buttons to the QWebEngineView's back and forward functions
        self.back_button.clicked.connect(self.view.back)
        self.forward_button.clicked.connect(self.view.forward)

        # Connect the refresh button to the QWebEngineView's reload function
        self.refresh_button.clicked.connect(self.view.reload)

        # Create a horizontal layout for the URL bar and buttons
        controls_layout = QtWidgets.QHBoxLayout()
        controls_layout.addWidget(self.back_button)
        controls_layout.addWidget(self.forward_button)
        controls_layout.addWidget(self.refresh_button)
        controls_layout.addWidget(self.url_bar)

        # Create a vertical layout for the QWebEngineView and the controls
        layout = QtWidgets.QVBoxLayout(self)
        layout.addLayout(controls_layout)
        layout.addWidget(self.view)

        # Set the main layout of the widget
        self.setLayout(layout)

    def load_url(self):
        # Get the URL or English word from the URL bar
        query = self.url_bar.text()

        # If the query is not empty, navigate to the appropriate URL
        if query:
            # Check if the query is an English word
            if query.lower() in vocabulary:
                # Navigate to a dictionary website
                self.view.setUrl(QtCore.QUrl(f"https://www.dictionary.com/browse/{query.lower()}"))
            elif query.lower() in grammar:
                # Navigate to a grammar website
                self.view.setUrl(QtCore.QUrl(f"https://www.englishgrammar.org/{query.lower()}"))
            elif query.lower() in search:
                self.view.setUrl(QtCore.QUrl(f"https://www.{query.lower()}.com/"))
            else:
                # Assume the query is a URL and navigate to it
                self.view.setUrl(QtCore.QUrl(query))

if __name__ == "__main__":
    app = QtWidgets.QApplication(sys.argv)
    browser = WebBrowser()
    browser.show()
    sys.exit(app.exec_())
